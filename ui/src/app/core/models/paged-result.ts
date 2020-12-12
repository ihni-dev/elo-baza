import { PagingInfo } from './paging-info';

export class PagedResult<T> {
  pagingInfo: PagingInfo;
  data: T[];

  constructor(pagingInfo: PagingInfo, data: T[]) {
    this.pagingInfo = pagingInfo;
    this.data = data;
  }

  public static empty<U>(): PagedResult<U> {
    return new PagedResult<U>(PagingInfo.empty(), []);
  }
}
