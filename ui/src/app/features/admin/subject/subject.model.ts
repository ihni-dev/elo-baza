import { PagedResult } from 'src/app/core/models/paged-result';
import { PagingInfo } from 'src/app/core/models/paging-info';

export class Subject {
  key: string;
  name: string;
}

export class SubjectResult extends PagedResult<Subject> {
  constructor(pagingInfo: PagingInfo, data: Subject[]) {
    super(pagingInfo, data);
  }
}
