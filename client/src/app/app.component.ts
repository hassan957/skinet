import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './model/pagination';
import { IProduct } from './model/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'client';
  products!: IProduct[];
  constructor(private http:HttpClient){}

  ngOnInit(): void {
    this.http.get("https://localhost:44362/api/Products").subscribe(
      (response: any)=> {
    this.products=response.data;
  }, error=>{
      console.log(error);
    }
    )
  }

}
