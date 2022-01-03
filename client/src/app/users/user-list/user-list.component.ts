import { Component, OnInit } from '@angular/core';
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
  congregations: Congregation[];
  users: User[];
  pagination: Pagination;
  userParams: UserParams;

  constructor(
    private userService: UsersService, 
    private congregationService: CongregationsService) {
    this.userParams = new UserParams();
   }

  ngOnInit(): void {
    this.loadCongregations();
    this.loadUsers();
  }

  loadCongregations() {
    this.congregationService.getCongregations().subscribe(response => {
      this.congregations = response;
    })
  }

  loadUsers() {
    this.userService.getUsers(this.userParams).subscribe(response => {
      this.users = response.result;
      this.pagination = response.pagination;
    });
  }

  resetFilters() {
    this.userParams = new UserParams();
    this.loadUsers();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.loadUsers();
  }

}
