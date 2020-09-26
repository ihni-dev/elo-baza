import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ThemePickerModule } from '../theme-picker/theme-picker.module';
import { NavbarComponent } from './navbar.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    MatButtonModule,
    MatMenuModule,
    RouterModule,
    ThemePickerModule
  ],
  exports: [NavbarComponent],
  declarations: [NavbarComponent]
})
export class NavbarModule {}
