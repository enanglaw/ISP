import { NativeDateAdapter } from '@angular/material/core';

export class AppDateAdapter extends NativeDateAdapter {
  parse(value: string): Date | null {
    // 15.07.18 -> [15, 7, 18]
    const values: string[] = value.replace(/[\.\\-]/g, '/').split('/');

    const date: number = Number(values[0]);
    const month: number = Number(values[1]) - 1;
    let year: number = Number(values[2]);

    if (!year) {
      // If year not set then the year is current year
      // e.g. 05/07 = 05/07/19
      year = new Date(Date.now()).getUTCFullYear();
    } else if (year < 1000) {
      // Without the fix 02 = 1902
      year += 2000; // Welcome to 21 century
    }

    // Invalid Date fix
    let parsedDate: Date | null = new Date(year, month, date);
    if (isNaN(parsedDate.getTime()) || date > 31 || month > 11) {
      parsedDate = null;
    }

    return parsedDate;
  }

  format(date: Date, displayFormat: string): string {
    if (displayFormat === 'input') {
      const day: number = date.getDate();
      const month: number = date.getMonth() + 1;
      const year: number = date.getFullYear();

      return this.to2digit(day) + '/' + this.to2digit(month) + '/' + year;
    } else if (displayFormat === 'inputMonth') {
      const month: number = date.getMonth() + 1;
      const year: number = date.getFullYear();

      return this.to2digit(month) + '/' + year;
    } else {
      return date.toDateString();
    }
  }

  private to2digit(n: number): string {
    return ('00' + n).slice(-2);
  }
}

export const APP_DATE_FORMATS = {
  parse: {
    dateInput: { month: 'short', year: 'numeric', day: 'numeric' },
  },
  display: {
    dateInput: 'input',
    monthYearLabel: 'inputMonth',
    dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
    monthYearA11yLabel: { year: 'numeric', month: 'long' },
  },
};
// https://github.com/angular/components/issues/14291
// https://stackoverflow.com/questions/55721254/how-to-change-mat-datepicker-date-format-to-dd-mm-yyyy-in-simplest-way
