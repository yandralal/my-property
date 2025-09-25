import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'inrFormat',
  standalone: true
})
export class InrFormatPipe implements PipeTransform {
  transform(value: number | string): string {
    if (value == null) return '';
    let num = typeof value === 'string' ? parseFloat(value.replace(/,/g, '')) : value;
    if (isNaN(num)) return '';
    return num.toLocaleString('en-IN', { maximumFractionDigits: 0 });
  }
}
