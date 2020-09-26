import { Component, OnInit } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { ChangePageTitle } from 'src/app/core/layout/page-title/page-title-actions';
import { PageTitleState } from 'src/app/core/layout/page-title/page-title-state';
import { Observable } from 'rxjs';
import { PageTitleStateModel } from 'src/app/core/layout/page-title/page-title-state.model';

@Component({
  selector: 'app-home-page',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss']
})
export class HomePage implements OnInit {
  @Select(PageTitleState.headerTitle) headerTitle$: Observable<string>;
  @Select(PageTitleState.headerSubtitle) headerSubtitle$: Observable<string>;

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(
      new ChangePageTitle('Home', 'Elo-Baza', 'Baza pytań do zakucia')
    );
  }
}
