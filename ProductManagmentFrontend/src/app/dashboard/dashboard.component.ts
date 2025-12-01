import { Component } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { ResponseCodeEnum } from '../enums/ResponseCodeEnum';
import { ResponseModel } from '../models/response-model';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  productsCount = 0;

  constructor(
    private productsService: ProductsService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadProductsCount();
  }

  loadProductsCount() {
    this.productsService
      .getProductsCount()
      .subscribe((response: ResponseModel) => {
        if (response && response.code == ResponseCodeEnum.Success) {
          this.productsCount = response.result;
        } else if (response && response.messageFL) {
          this.toastr.error(response.messageFL, 'Error');
        }
      });
  }
}
