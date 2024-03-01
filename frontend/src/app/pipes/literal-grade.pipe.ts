import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'literalGrade',
  standalone: true,
})
export class LiteralGradePipe implements PipeTransform {
  transform(value: number, ...args: any): any {
    if (value >= 90) {
      return 'A';
    } else if (value >= 80 || value > 89) {
      return 'B';
    } else if (value >= 70 || value > 79) {
      return 'C';
    } else if (value >= 0 || value > 69) {
      return 'F';
    } else return '';
  }
}
