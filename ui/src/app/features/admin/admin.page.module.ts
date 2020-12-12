import { NgModule } from '@angular/core';
import { AdminPage } from './admin.page';
import { Routes, RouterModule } from '@angular/router';
import { FooterModule } from 'src/app/core/layout/footer/footer.module';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { PageHeaderModule } from 'src/app/core/layout/page-header/page-header.module';
import { SubjectComponent } from './subject/subject.component';
import { SubjectService } from './subject/subject.service';
import { CategoryService } from './category/category.service';
import { CategoryComponent } from './category/category.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgxsModule } from '@ngxs/store';
import { SubjectState } from './subject/subject.state';

const routes: Routes = [{ path: '', component: AdminPage }];

@NgModule({
  imports: [
    CommonModule,
    FooterModule,
    PageHeaderModule,
    MatButtonModule,
    MatCardModule,
    MatTabsModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatPaginatorModule,
    MatTableModule,
    FlexLayoutModule,
    RouterModule.forChild(routes),
    NgxsModule.forFeature([SubjectState])
  ],
  declarations: [AdminPage, SubjectComponent, CategoryComponent],
  providers: [SubjectService, CategoryService]
})
export class AdminModule {}
