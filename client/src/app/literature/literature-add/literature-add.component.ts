import { Component, OnInit, Output, EventEmitter } from '@angular/core';
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

  constructor(
    private litService: LiteratureService, 
    private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
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
