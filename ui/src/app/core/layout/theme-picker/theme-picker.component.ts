import {
  ChangeDetectionStrategy,
  Component,
  ViewEncapsulation
} from '@angular/core';
import { StyleManagerService } from './services/style-manager.service';
import { ThemeStorageService } from './services/theme-storage.service';
import { ThemeModel } from './models/theme.model';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import { THEMES } from './models/themes';

@Component({
  selector: 'app-theme-picker',
  templateUrl: 'theme-picker.component.html',
  styleUrls: ['theme-picker.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None
})
export class ThemePickerComponent {
  themes = THEMES;
  currentTheme: ThemeModel;

  constructor(
    public styleManager: StyleManagerService,
    private themeStorage: ThemeStorageService,
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer
  ) {
    iconRegistry.addSvgIcon(
      'theme-example',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/theme-picker.svg')
    );

    const defaultThemeName = this.themes.find((t) => t.isDefault).name;
    const themeName =
      this.themeStorage.getStoredThemeName() ?? defaultThemeName;

    this.selectTheme(themeName);
    this.currentTheme = this.themes.find((t) => t.name === themeName);
  }

  selectTheme(themeName: string) {
    const theme = this.themes.find((t) => t.name === themeName);

    if (!theme) {
      return;
    }

    this.currentTheme = theme;

    if (theme.isDefault) {
      this.styleManager.removeStyle('theme');
    } else {
      this.styleManager.setStyle('theme', `assets/${theme.name}.css`);
    }

    if (this.currentTheme) {
      this.themeStorage.storeTheme(this.currentTheme);
    }
  }
}
