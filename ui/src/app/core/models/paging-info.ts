export class PagingInfo {
  totalCount: number;
  page: number;
  pageSize: number;
  lastPage: number;
  get hesPrevious(): boolean {
    return this.page > 1;
  }
  get hasNext(): boolean {
    return this.page < this.lastPage;
  }

  constructor(
    totalCount: number,
    page: number,
    pageSize: number,
    lastPage: number
  ) {
    this.totalCount = totalCount;
    this.page = page;
    this.pageSize = pageSize;
    this.lastPage = lastPage;
  }

  static empty(): PagingInfo {
    return new PagingInfo(0, 0, 0, 0);
  }
}
