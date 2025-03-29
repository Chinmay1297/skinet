import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormField,
    MatError,
    MatLabel,
    MatInput
  ],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent implements ControlValueAccessor{
  @Input() label = '';
  @Input() type = 'text';

  //Generally services are shared
  //@Self() is used to prevent the dependency injected objected to be shared/reused with other inputs of same component
  //Because we want this to belong to a single input (not multiple instances of this component)
  constructor(@Self() public controlDir: NgControl){  
    this.controlDir.valueAccessor = this;
  }
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  get control(){
    return this.controlDir.control as FormControl
  }
}
