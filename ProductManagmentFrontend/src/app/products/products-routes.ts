import { Routes } from '@angular/router';

export const PRODUCTS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./products-list/products-list.component').then(
        (c) => c.ProductsListComponent
      ),
  },
  {
    path: 'details',
    loadComponent: () =>
      import('./product-add-edit/product-add-edit.component').then(
        (c) => c.ProductAddEditComponent
      ),
  },
  {
    path: 'details/:id',
    loadComponent: () =>
      import('./product-add-edit/product-add-edit.component').then(
        (c) => c.ProductAddEditComponent
      ),
  },
];
