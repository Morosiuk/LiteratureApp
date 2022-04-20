import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Congregation } from 'src/app/_models/congregation';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/Params/userParams';
import { CongregationsService } from 'src/app/_services/congregations.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-congregation-item',
  templateUrl: './congregation-item.component.html',
  styleUrls: ['./congregation-item.component.css']
})
export class CongregationItemComponent implements OnInit {
  congregation: Congregation;
  userParams: UserParams;
  users: User[];
  pagination: Pagination;

  constructor(
    private userService: UsersService,
    private congregationService: CongregationsService,
    private route: ActivatedRoute) { 
      this.userParams = new UserParams();
    }

  ngOnInit(): void {
    this.loadCongregation();
  }

  loadCongregation() {
    this.congregationService.getCongregation(this.route.snapshot.paramMap.get('name')).subscribe(cong => {
      console.log(cong);
      this.congregation = cong;
      this.userParams.congregation = cong.id;
      this.LoadPublishers();
    });
  }

  LoadPublishers() {
    console.log(this.userParams);
    this.userService.getUsers(this.userParams).subscribe(response => {
      this.users = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.userService.setUserParams(this.userParams);
    this.LoadPublishers();
  }

}
