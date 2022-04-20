import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CongParams } from 'src/app/_models/Params/congParams';
import { CongregationCard } from 'src/app/_models/congregationCard';
import { CongregationsService } from 'src/app/_services/congregations.service';

@Component({
  selector: 'app-congregation-add',
  templateUrl: './congregation-add.component.html',
  styleUrls: ['./congregation-add.component.css']
})
export class CongregationAddComponent implements OnInit {
  congregations: CongregationCard[];
  addForm: FormGroup;
  validationErrors: string[] = [];
  congParams: CongParams;

  constructor(
    private congregationService: CongregationsService, 
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.initForm();
    this.getCongregations();
    this.congParams = new CongParams();
  }
  
  initForm() {
    this.addForm = this.formBuilder.group({
      name: ['', [Validators.required, this.uniqueName()]],
      confirmName: ['', [Validators.required, this.matchValues('name')]],
      code: ['', Validators.min(0)]
    });
    this.addForm.controls.name.valueChanges.subscribe(() => {
      this.addForm.controls.confirmName.updateValueAndValidity();
    });
  }

  getCongregations() {
    this.congregationService.getCongregations(this.congParams).subscribe(response => {
      this.congregations = response.result;
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null : {isMatching: true}
    }
  }

  uniqueName(): ValidatorFn {
    return (control: AbstractControl) => {
      return this.congregations?.some(c => c.name === control?.value)
        ? {uniqueName: true} : null 
    }
  }

  addCongregation() {
    //Create model to send to api from form
    const codeValue: string = this.addForm.value?.code;
    const newCongregation: any = {
      name: this.addForm.value.name,
      code: codeValue?.length > 0 ? codeValue : null 
    };

    //post new congregation details
    this.congregationService.addCongregation(newCongregation).subscribe(response => {
      this.router.navigateByUrl("/congregations");
      this.toastr.success("Congregation added.");
    }, error => {
      this.validationErrors = error;
    });
  }

  cancel() {
    this.router.navigateByUrl("/congregations");
  }

}
