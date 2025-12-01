import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { ToastrService } from 'ngx-toastr';

import { environment } from '../../../environments/environment';
import { ProductsService } from '../../services/products.service';
import { ResponseCodeEnum } from '../../enums/ResponseCodeEnum';
import { ResponseModel } from '../../models/response-model';
import { ProductModel } from '../../models/product-model';

@Component({
  selector: 'app-products-list',
  imports: [
    TableModule,
    FormsModule,
    RouterModule,
    IconFieldModule,
    InputIconModule,
    DialogModule,
    ButtonModule,
  ],
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.scss',
})
export class ProductsListComponent {
  list: any[] = [];

  totalRecords: number = 0;

  dataTable = {
    limit: environment.paging,
    count: 0,
    offset: 0,
    orderBy: 'id',
    orderDir: -1,
    search: '',
  };

  first: number = 0;
  search: string = '';
  deletePopup: boolean = false;
  productToBeDeletedId: number = 0;
  productToBeDeletedName: string = '';

  constructor(
    private productsService: ProductsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.reloadData();
  }

  reloadData() {
    const params = new HttpParams()
      .set('orderColumn', `${this.dataTable.orderBy}`)
      .set('orderDir', `${this.dataTable.orderDir}`)
      .set('pageNumber', `${this.dataTable.offset}`)
      .set('pageSize', `${this.dataTable.limit}`)
      .set('search', `${this.dataTable.search}`);

    this.productsService
      .getProductsPages(params)
      .subscribe((response: ResponseModel) => {
        if (response && response.code == ResponseCodeEnum.Success) {
          this.list = response['result']['data'];
          this.totalRecords = response['result']['totalRecords'];
        } else if (response && response.messageFL) {
          this.toastr.error(response.messageFL, 'Error');
        }
      });
  }

  nextPage(event: TableLazyLoadEvent) {
    this.dataTable.offset =
      event.first == 0 ? 0 : (event.first ?? 0) / (event.rows ?? 0);
    this.dataTable.orderBy = (
      event.sortField ? event.sortField : 'id'
    ) as string;
    this.dataTable.orderDir = event.sortOrder as number;
    this.reloadData();
  }

  searchAction() {
    this.first = 0;
    this.dataTable.offset = 0;
    this.dataTable.count = 0;

    this.dataTable.search = this.search;

    return this.reloadData();
  }

  deleteProduct() {
    this.productsService
      .deleteProduct(this.productToBeDeletedId)
      .subscribe((response: ResponseModel) => {
        if (response && response.code == ResponseCodeEnum.Success) {
          this.reloadData();
          this.toastr.success('Deleted successfully!', 'Success');
          this.hideDeleteProductPopup();
        } else if (response && response.messageFL) {
          this.toastr.error(response.messageFL, 'Error');
        }
      });
  }

  deleteProductConfirmation(product: ProductModel) {
    this.productToBeDeletedId = product.id;
    this.productToBeDeletedName = product.name;
    this.deletePopup = true;
  }

  hideDeleteProductPopup() {
    this.deletePopup = false;
  }
}
