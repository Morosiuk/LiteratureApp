import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LiteratureService } from 'src/app/_services/literature.service';

@Component({
  selector: 'app-literature-add',
  templateUrl: './literature-add.component.html',
  styleUrls: ['./literature-add.component.css']
})
export class LiteratureAddComponent implements OnInit {
  model: any = {};
  literatureForm: FormGroup;
  numberRegEx = /^[0-9]*$/;

  constructor(
    private litService: LiteratureService, 
    private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initialiseForm();
  }

  initialiseForm() {
    this.literatureForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(4)]),
      fullName: new FormControl(''),
      itemId: new FormControl(),
      symbol: new FormControl('', [Validators.required, Validators.minLength(2)]),
      editions: new FormControl('', Validators.pattern(this.numberRegEx))
    });
  }

  add() {
    this.litService.addLiterature(this.model).subscribe(response => {
      console.log(response);
      this.toastr.success("literature added!")
      this.cancel();
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    })
  }

  cancel() {
    this.router.navigateByUrl('/');
  }

}
