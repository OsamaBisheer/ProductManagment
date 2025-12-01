import { Component } from '@angular/core';
import {
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
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
  selector: 'app-register',
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
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  hide = true;

  form?: any;

  constructor(
    private usersService: UsersService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.form = new FormGroup(
      {
        userName: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', Validators.required),
        confirmPassword: new FormControl('', Validators.required),
        phoneNumber: new FormControl('', numbersOnlyValidator),
      },
      { validators: passwordMatchValidator }
    );
  }

  submit() {
    if (this.form.invalid) return;

    const formObj: any = this.form?.getRawValue();
    this.usersService.addUser(formObj).subscribe((response: ResponseModel) => {
      if (response && response.code == ResponseCodeEnum.Success) {
        this.router.navigate(['/login']);
        this.toastr.success('Registered successfully!', 'Success');
      } else if (response && response.messageFL) {
        this.toastr.error(response.messageFL, 'Error');
      }
    });
  }
}

export function numbersOnlyValidator(
  control: AbstractControl
): ValidationErrors | null {
  const value = control.value;
  if (value && !/^[0-9]+$/.test(value)) {
    return { numbersOnly: true };
  }
  return null;
}

export const passwordMatchValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;

  if (password && confirmPassword && password !== confirmPassword) {
    return { passwordMismatch: true };
  }

  return null; // valid
};
