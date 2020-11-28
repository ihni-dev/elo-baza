import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfigurationService } from '../../../core/app-configuration/app-configuration.service';
import { Observable } from 'rxjs';
import { CategoryResult } from './category.model';

@Injectable()
export class CategoryService {
  constructor(
    private httpClient: HttpClient,
    private appConfiguration: AppConfigurationService
  ) {}

  public getCategories(): Observable<CategoryResult> {
    const url = this.appConfiguration.combineWithApiUrl(
      'subject',
      '4670c071-34e0-4475-9e91-14e8082df39e',
      'category'
    );

    return this.httpClient.get<CategoryResult>(url);
  }
}
