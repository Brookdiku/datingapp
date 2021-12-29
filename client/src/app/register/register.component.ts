import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private accountService: AccountService,private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  registerUser() {
    this.accountService.register(this.model).subscribe(resoponse => {
      console.log(resoponse);
      this.cancel();
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    });
  }
  cancel() {
    this.cancleRegister.emit(false);
  }
}
