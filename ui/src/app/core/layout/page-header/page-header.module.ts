import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PageHeaderComponent } from './page-header';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [CommonModule, MatButtonModule, MatIconModule],
  exports: [PageHeaderComponent],
  declarations: [PageHeaderComponent]
})
export class PageHeaderModule {}
