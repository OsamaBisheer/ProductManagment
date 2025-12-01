import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

import { logout } from '../shared/methods';

@Component({
  selector: 'app-main',
  imports: [RouterModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {
  constructor(private router: Router) {}

  logout() {
    logout();

    this.router.navigate(['/login']);
  }
}
