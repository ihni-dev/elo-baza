import { Component, NgModule, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from 'src/app/core/layout/footer/footer.module';
import { SvgViewerModule } from 'src/app/shared/svg-viewer/svg-viewer.module';
import { PageTitleService } from 'src/app/core/services/page-title.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  constructor(public _pageTitleService: PageTitleService) {}

  ngOnInit(): void {
    this._pageTitleService.title = '';
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
