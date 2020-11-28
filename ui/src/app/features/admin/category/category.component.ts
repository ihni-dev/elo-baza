import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryResult } from './category.model';
import { CategoryService } from './category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  constructor(public categoryService: CategoryService) {}

  public categoryResult: Observable<CategoryResult>;
  displayedColumns: string[] = ['key', 'parentKey', 'name'];

  ngOnInit(): void {
    this.categoryResult = this.categoryService.getCategories();
  }
}
