import { Component, NgModule, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from 'src/app/core/layout/footer/footer.module';
import { SvgViewerModule } from 'src/app/shared/svg-viewer/svg-viewer.module';
import { Store, Select } from '@ngxs/store';
import { ChangePageTitle } from 'src/app/core/services/page-title/page-title-actions';
import { PageTitleState } from 'src/app/core/services/page-title/page-title-state';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home-page',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  @Select(PageTitleState.pageTitle) pageTitle$: Observable<string>;
  pageTitle: string;

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(new ChangePageTitle('Home'));
    this.pageTitle$.subscribe((pageTitle) => (this.pageTitle = pageTitle));
  }
}

const routes: Routes = [{ path: '', component: HomePage }];

@NgModule({
  imports: [
    SvgViewerModule,
    MatButtonModule,
    FooterModule,
    RouterModule.forChild(routes),
  ],
  exports: [HomePage],
  declarations: [HomePage],
})
export class HomePageModule {}
