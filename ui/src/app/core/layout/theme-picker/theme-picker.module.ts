import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ThemePickerComponent } from './theme-picker.component';
import { StyleManagerService } from './services/style-manager.service';
import { ThemeStorageService } from './services/theme-storage.service';

@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatTooltipModule,
  ],
  exports: [ThemePickerComponent],
  declarations: [ThemePickerComponent],
  providers: [StyleManagerService, ThemeStorageService],
})
export class ThemePickerModule {}
