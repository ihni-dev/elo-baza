import { NgModule } from '@angular/core';
import { AdminPage } from './admin.page';
import { Routes, RouterModule } from '@angular/router';
import { FooterModule } from 'src/app/core/layout/footer/footer.module';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { PageHeaderModule } from 'src/app/core/layout/page-header/page-header.module';

const routes: Routes = [{ path: '', component: AdminPage }];

@NgModule({
  imports: [
    CommonModule,
    FooterModule,
    PageHeaderModule,
    MatButtonModule,
    MatTabsModule,
    MatTableModule,
    RouterModule.forChild(routes),
  ],
  declarations: [AdminPage],
})
export class AdminModule {}
