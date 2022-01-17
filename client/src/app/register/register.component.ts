import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { CongParams } from '../_models/congParams';
import { CongregationCard } from '../_models/congregationCard';
import { AccountService } from '../_services/account.service';
import { CongregationsService } from '../_services/congregations.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup;
  congregations: CongregationCard[];
  congParams: CongParams;

  constructor(private accountService: AccountService,
    private formBuilder: FormBuilder,
    private congregationService: CongregationsService) { 
      this.congParams = new CongParams();
    }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.loadCongregations();
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      firstName: ['', Validators.required],
      surname: ['', Validators.required],
      congregation: [null, Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
        ? null : {isMatching: true}
    }
  }

  loadCongregations() {
    this.congregationService.getCongregations(this.congParams).subscribe(response => {
      this.congregations = response.result;
    })
  }

  register() {
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe(response => {
    //   console.log(response);
    //   this.cancel();
    // }, error => {
    //   console.log(error);
    //   this.toastr.error(error.error);
    // })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
