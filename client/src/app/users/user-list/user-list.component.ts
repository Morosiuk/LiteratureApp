import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { CongParams } from 'src/app/_models/congParams';
import { Congregation } from 'src/app/_models/congregation';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { CongregationsService } from 'src/app/_services/congregations.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  currentUser: User;
  congregations: Congregation[];
  users: User[];
  pagination: Pagination;
  userParams: UserParams;
  congParams: CongParams;

  constructor(
    private userService: UsersService, 
    private congregationService: CongregationsService) {
      this.userParams = this.userService.getUserParams();
      this.congParams = new CongParams();
   }

  ngOnInit(): void {
    this.loadCongregations();
    this.loadUsers();
  }

  loadCongregations() {
    this.congregationService.getCongregations(this.congParams).subscribe(response => {
      this.congregations = response.result;
    })
  }

  loadUsers() {
    this.userService.setUserParams(this.userParams);
    this.userService.getUsers(this.userParams).subscribe(response => {
      this.users = response.result;
      this.pagination = response.pagination;
    });
  }

  resetFilters() {
    this.userParams = this.userService.resetUserParams();
    this.loadUsers();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.userService.setUserParams(this.userParams);
    this.loadUsers();
  }

}
