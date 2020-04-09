export interface Section {
  name: string;
  summary: string;
}

const FIRST = 'admin';
export const SECTIONS: { [key: string]: Section } = {
  [FIRST]: {
    name: 'Admin',
    summary: 'Admin section summary',
  },
};
