import {Injectable, EventEmitter} from '@angular/core';

export interface SiteTheme {
  name: string;
  displayName?: string;
  accent: string;
  primary: string;
  isDark?: boolean;
  isDefault?: boolean;
}


@Injectable()
export class ThemeStorageService {
  static storageKey = 'theme-storage-current-name';

  onThemeUpdate: EventEmitter<SiteTheme> = new EventEmitter<SiteTheme>();

  storeTheme(theme: SiteTheme) {
    try {
      window.localStorage[ThemeStorageService.storageKey] = theme.name;
    } catch { }

    this.onThemeUpdate.emit(theme);
  }

  getStoredThemeName(): string | null {
    try {
      return window.localStorage[ThemeStorageService.storageKey] || null;
    } catch {
      return null;
    }
  }

  clearStorage() {
    try {
      window.localStorage.removeItem(ThemeStorageService.storageKey);
    } catch { }
  }
}
