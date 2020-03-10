import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ArrayType } from '@angular/compiler';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent {
  public products: Array<any>;
  noimage:string='noimage.jpg';

  constructor(http:HttpClient){

    http.get<any[]>(environment.apiBaseURI + 'Products').subscribe(result => {
        this.products = result;
      }, error => console.error(error));
  }

  getImage (prod:any):string {
    return prod.productImage || 'noimage.jpg';
  }
}


