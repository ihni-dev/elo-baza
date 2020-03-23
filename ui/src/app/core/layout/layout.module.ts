import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { StyleManagerService } from './theme-picker/services/style-manager.service';
import { ThemeStorageService } from './theme-picker/services/theme-storage.service';
import { ThemePickerComponent } from './theme-picker/theme-picker.component';



@NgModule({
  declarations: [
    ThemePickerComponent,
    NavbarComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent
  ],
  providers: [
    StyleManagerService,
    ThemeStorageService
  ]
})
export class LayoutModule { }
