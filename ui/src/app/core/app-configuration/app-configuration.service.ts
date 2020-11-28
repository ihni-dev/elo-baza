import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfiguration } from './app-configuration.model';

@Injectable({
  providedIn: 'root'
})
export class AppConfigurationService {
  private configuration: AppConfiguration = {
    apiUrl: ''
  };

  constructor(private http: HttpClient) {}

  public loadConfiguration(): void {
    this.http
      .get<AppConfiguration>('/assets/app-configuration.json')
      .subscribe((res) => (this.configuration = res));
  }

  public getConfig(): AppConfiguration {
    return this.configuration;
  }

  public combineWithApiUrl(...segments: string[]): string {
    let url = this.getConfig().apiUrl;
    if (url.endsWith('/')) url = url.slice(0, -1);

    segments.forEach((segment) => {
      url += `/${segment}`;
    });

    return url;
  }
}
