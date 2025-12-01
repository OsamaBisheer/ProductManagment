import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../environments/environment';
import { LoginModel } from '../models/login-model';
import { AddUserModel } from '../models/add-user-model';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(public http: HttpClient) {}

  login(model: LoginModel) {
    return this.http.post(environment.hostUrl + `api/user/login`, model);
  }

  logout(id: number) {
    return this.http.delete(environment.hostUrl + `api/user/logout`);
  }

  addUser(model: AddUserModel) {
    return this.http.post(environment.hostUrl + `api/user/add-user`, model);
  }
}
