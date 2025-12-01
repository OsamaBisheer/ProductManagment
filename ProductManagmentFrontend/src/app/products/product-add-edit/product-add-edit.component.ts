import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

import { ProductsService } from '../../services/products.service';
import { ResponseCodeEnum } from '../../enums/ResponseCodeEnum';
import { ResponseModel } from '../../models/response-model';

@Component({
  selector: 'app-product-add-edit',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    CommonModule,
  ],
  templateUrl: './product-add-edit.component.html',
  styleUrl: './product-add-edit.component.scss',
})
export class ProductAddEditComponent implements OnInit {
  id: number = 0;
  formData?: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productsService: ProductsService,
    private toastr: ToastrService
  ) {
    this.formData = new FormGroup({
      name: new FormControl('', Validators.required),
      price: new FormControl(1, [Validators.required, Validators.min(0.1)]),
      description: new FormControl(''),
    });

    this.formData.disable();

    this.route.params.subscribe((data) => {
      this.id = data['id'] ? data['id'] : 0;
      if (this.id) this.load();
      else this.formData.enable();
    });
  }

  ngOnInit() {}

  load() {
    this.productsService
      .getProductById(this.id)
      .subscribe((response: ResponseModel) => {
        if (response && response.code == ResponseCodeEnum.Success) {
          this.formData?.patchValue(response.result);
          this.formData.enable();
        } else {
          if (response && response.messageFL) {
            this.toastr.error(response.messageFL, 'Error');
          }
          this.navigateToBack();
        }
      });
  }

  submit() {
    if (this.formData?.valid) {
      if (this.id) this.update();
      else this.create();
    }
  }

  create() {
    const formObj: any = this.formData?.getRawValue();
    this.productsService
      .createProduct(formObj)
      .subscribe((response: ResponseModel) => {
        if (response && response.code == ResponseCodeEnum.Success) {
          this.navigateToBack();
          this.toastr.success('Created successfully!', 'Success');
        } else if (response && response.messageFL) {
          this.toastr.error(response.messageFL, 'Error');
        }
      });
  }

  update() {
    const formObj: any = this.formData?.getRawValue();
    formObj.id = this.id;
    this.productsService
      .updateProduct(formObj)
      .subscribe((response: ResponseModel) => {
        if (response && response.code == ResponseCodeEnum.Success) {
          this.navigateToBack();
          this.toastr.success('Updated successfully!', 'Success');
        } else if (response && response.messageFL) {
          this.toastr.error(response.messageFL, 'Error');
        }
      });
  }

  navigateToBack() {
    this.router.navigate(['/products']);
  }
}
