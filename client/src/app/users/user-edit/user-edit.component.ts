import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  user: User;
  currentUser: User;

  constructor(
    private accountService: AccountService,
    private userService: UsersService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
  }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser() {
    this.userService.getUser(this.currentUser.username).subscribe(user => {
      this.user = user;
    })
  }

}
