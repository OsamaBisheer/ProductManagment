import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../environments/environment';
import { ProductModel } from '../models/product-model';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  constructor(public http: HttpClient) {}

  getProductsCount() {
    return this.http.get(environment.hostUrl + `api/product/count`);
  }

  getProductsPages(params: any) {
    return this.http.get(environment.hostUrl + 'api/product/search', {
      params,
    });
  }

  getProductById(id: number) {
    return this.http.get(environment.hostUrl + `api/product/by-id/${id}`);
  }

  createProduct(product: ProductModel) {
    return this.http.post(environment.hostUrl + `api/product/create`, product);
  }

  updateProduct(product: ProductModel) {
    return this.http.put(environment.hostUrl + `api/product/update`, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(environment.hostUrl + `api/product/delete/${id}`);
  }
}
