import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancleRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }
  registerUser() {
    this.accountService.register(this.model).subscribe(resoponse => {
      console.log(resoponse);
    }, error => {
      console.log(error);
    });
  }
  cancel() {
    this.cancleRegister.emit(false);
  }
}
