import { Component } from '@angular/core';
import {
  Validators,
  ReactiveFormsModule,
  FormGroup,
  FormControl,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

import { UsersService } from '../../services/users.service';
import { ResponseModel } from '../../models/response-model';
import { ResponseCodeEnum } from '../../enums/ResponseCodeEnum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  hide = true;

  form?: any;

  constructor(
    private usersService: UsersService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.form = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }

  submit() {
    if (this.form.invalid) return;

    const formObj: any = this.form?.getRawValue();
    this.usersService.login(formObj).subscribe((response: ResponseModel) => {
      if (response && response.code == ResponseCodeEnum.Success) {
        localStorage.setItem('token', response.result);
        this.router.navigate(['/dashboard']);
        this.toastr.success('Logged in successfully!', 'Success');
      } else if (response && response.messageFL) {
        this.toastr.error(response.messageFL, 'Error');
      }
    });
  }
}
