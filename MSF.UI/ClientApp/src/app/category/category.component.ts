import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";

@Component({
    selector:'app-category',
    templateUrl:'./category.component.html'
})
export class CategoryComponent{
    public Categories : Category[];

    constructor(http:HttpClient){
        http.get<Category[]>(environment.apiBaseURI + 'Categories').subscribe(result => {
            this.Categories = result;
          }, error => console.error(error));
    }
}

interface Category{
    Id : number,
    categoryName:string,
    rowVersion:string,
    imageURL:string
}