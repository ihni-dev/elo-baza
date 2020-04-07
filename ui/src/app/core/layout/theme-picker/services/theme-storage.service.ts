import { Injectable } from '@angular/core';
import { ThemeModel } from '../models/theme.model';

@Injectable()
export class ThemeStorageService {
  static storageKey = 'theme-storage-current-name';

  storeTheme(theme: ThemeModel) {
    try {
      window.localStorage[ThemeStorageService.storageKey] = theme.name;
    } catch {}
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
    } catch {}
  }
}
