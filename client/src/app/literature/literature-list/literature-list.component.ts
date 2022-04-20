import { Component, OnInit } from '@angular/core';
import { Literature } from 'src/app/_models/literature';
import { LitParams } from 'src/app/_models/Params/litParams';
import { LiteratureService } from 'src/app/_services/literature.service';

@Component({
  selector: 'app-literature-list',
  templateUrl: './literature-list.component.html',
  styleUrls: ['./literature-list.component.css']
})
export class LiteratureListComponent implements OnInit {
  literature: Literature[];
  litParams: LitParams;

  constructor(private literatureService: LiteratureService) { }

  ngOnInit(): void {
    this.loadLiterature();
  }

  loadLiterature() {
    this.literatureService.getLiterature(this.litParams).subscribe(response => {
      this.literature = response.result;
    });
  }

}
