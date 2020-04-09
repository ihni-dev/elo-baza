import { Component } from '@angular/core';
import { Select } from '@ngxs/store';
import { PageTitleState } from '../page-title/page-title-state';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.html',
  styleUrls: ['./page-header.scss'],
})
export class PageHeaderComponent {
  @Select(PageTitleState.headerTitle) headerTitle$: Observable<string>;
}
