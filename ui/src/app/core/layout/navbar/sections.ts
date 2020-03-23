export interface Section {
    name: string;
    summary: string;
}

const FIRST = 'first';
const SECOND = 'second';
const THIRD = 'third';
export const SECTIONS: {[key: string]: Section} = {
  [FIRST]: {
    name: 'First',
    summary: 'First section summary'
  },
  [SECOND]: {
    name: 'Second',
    summary: 'Second section summary'
  },
  [THIRD]: {
    name: 'Third',
    summary: 'Third section summary'
  }
};
