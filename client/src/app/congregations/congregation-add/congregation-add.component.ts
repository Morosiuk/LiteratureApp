import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CongregationSummary } from 'src/app/_models/congregationSummary';
import { CongregationsService } from 'src/app/_services/congregations.service';

@Component({
  selector: 'app-congregation-add',
  templateUrl: './congregation-add.component.html',
  styleUrls: ['./congregation-add.component.css']
})
export class CongregationAddComponent implements OnInit {
  @Output() cancelAdd = new EventEmitter();
  @Input() congregations: CongregationSummary[];
  addForm: FormGroup;

  constructor(
    private congregationService: CongregationsService, 
    private toastr: ToastrService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.addForm = this.formBuilder.group({
      name: ['', [Validators.required, this.uniqueName('name')]],
      confirmName: ['', [Validators.required, this.matchValues('name')]],
      code: ['', Validators.min(0)]
    });
    this.addForm.controls.name.valueChanges.subscribe(() => {
      this.addForm.controls.confirmName.updateValueAndValidity();
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
        ? null : {isMatching: true}
    }
  }

  uniqueName(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return this.congregations?.includes(control?.value)
        ? null : {uniqueName: true}
    }
  }

  addCongregation() {

  }

  cancel() {
    this.cancelAdd.emit(false);
  }
}
