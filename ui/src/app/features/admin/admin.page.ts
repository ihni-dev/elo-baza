import { Component, OnInit } from '@angular/core';
import { PageTitleState } from 'src/app/core/layout/page-title/page-title-state';
import { Observable } from 'rxjs';
import { Store, Select } from '@ngxs/store';
import { ChangePageTitle } from 'src/app/core/layout/page-title/page-title-actions';

@Component({
  templateUrl: 'admin.page.html',
  styleUrls: ['./admin.page.scss']
})
export class AdminPage implements OnInit {
  @Select(PageTitleState.headerTitle) headerTitle$: Observable<string>;
  @Select(PageTitleState.headerSubtitle) headerSubtitle$: Observable<string>;

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(
      new ChangePageTitle('Admin', 'Admin', 'Zarządzaj aplikacją')
    );
  }
}
