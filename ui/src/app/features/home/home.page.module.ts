import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from 'src/app/core/layout/footer/footer.module';
import { SvgViewerModule } from 'src/app/shared/svg-viewer/svg-viewer.module';
import { HomePage } from './home.page';
import { CommonModule } from '@angular/common';

const routes: Routes = [{ path: '', component: HomePage }];

@NgModule({
  imports: [
    CommonModule,
    SvgViewerModule,
    MatButtonModule,
    FooterModule,
    RouterModule.forChild(routes)
  ],
  declarations: [HomePage]
})
export class HomeModule {}
