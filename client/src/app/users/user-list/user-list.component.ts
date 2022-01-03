import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;

  constructor(private userService: UsersService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pageNumber, this.pageSize).subscribe(response => {
      this.users = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadUsers();
  }

}
