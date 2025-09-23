import { Component, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { PropertyService } from '../services/property.service';
import { RegisterPropertyRequest } from '../models/property.model';
import { CommonModule } from '@angular/common';
import { MessageBoxComponent } from '../shared/message-box.component';

@Component({
    selector: 'app-property-form',
    templateUrl: './property-form.component.html',
    styleUrls: ['./property-form.component.css'],
    standalone: true,
    imports: [ReactiveFormsModule, CommonModule, MessageBoxComponent]
})
export class PropertyFormComponent {
    messageBoxVisible: boolean = false;
    messageText: string = '';
    propertyForm;
    @Input() property: any = null;
    @Input() viewMode: boolean = false;

    constructor(private fb: FormBuilder, private propertyService: PropertyService) {
        this.propertyForm = this.fb.group({
            title: ['', [Validators.required, Validators.minLength(3), this.noWhitespaceValidator]],
            type: ['', [Validators.required, this.noWhitespaceValidator]],
            status: ['', [Validators.required, this.noWhitespaceValidator]],
            price: [null, [Validators.required, Validators.min(0)]],
            owner: ['', [Validators.required, Validators.minLength(3), this.ownerNameValidator]],
            phone: ['', [Validators.required, this.noWhitespaceValidator]],
            address: [''],
            city: [''],
            state: [''],
            zipCode: ['', [this.zipCodeValidator]],
            description: [''],
            khasraNo: [''],
            area: [null, [Validators.required, Validators.min(0)]]
        });
    }

    noWhitespaceValidator(control: AbstractControl): ValidationErrors | null {
        const isWhitespace = (control.value || '').toString().trim().length === 0;
        return !isWhitespace ? null : { whitespace: true };
    }

    ownerNameValidator(control: AbstractControl): ValidationErrors | null {
        const value = (control.value || '').toString();
        if (value.trim().length < 3) return { minLength: true };
        if (!/^[a-zA-Z ]+$/.test(value)) return { invalidOwner: true };
        return null;
    }

    zipCodeValidator(control: AbstractControl): ValidationErrors | null {
        const value = (control.value || '').toString();
        if (!value) return null;
        if (!/^\d{6}$/.test(value)) return { invalidZip: true };
        return null;
    }

    ngOnInit() {
        if (this.property) {
            this.propertyForm.patchValue({
                title: this.property.title ?? '',
                type: this.property.type ?? '',
                status: this.property.status ?? '',
                price: this.property.buyPrice ?? this.property.price ?? 0,
                owner: this.property.owner ?? '',
                phone: this.property.phone ?? '',
                address: this.property.address ?? '',
                city: this.property.city ?? '',
                state: this.property.state ?? '',
                zipCode: this.property.zipCode ?? '',
                description: this.property.description ?? '',
                khasraNo: this.property.khasraNo ?? '',
                area: this.property.area ?? 0
            });
            if (this.viewMode) {
                this.propertyForm.disable();
            } else {
                this.propertyForm.enable();
            }
        }
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['property'] && changes['property'].currentValue) {
            this.propertyForm.patchValue({
                title: changes['property'].currentValue.title ?? '',
                type: changes['property'].currentValue.type ?? '',
                status: changes['property'].currentValue.status ?? '',
                price: changes['property'].currentValue.buyPrice ?? changes['property'].currentValue.price ?? 0,
                owner: changes['property'].currentValue.owner ?? '',
                phone: changes['property'].currentValue.phone ?? '',
                address: changes['property'].currentValue.address ?? '',
                city: changes['property'].currentValue.city ?? '',
                state: changes['property'].currentValue.state ?? '',
                zipCode: changes['property'].currentValue.zipCode ?? '',
                description: changes['property'].currentValue.description ?? '',
                khasraNo: changes['property'].currentValue.khasraNo ?? '',
                area: changes['property'].currentValue.area ?? 0
            });
            if (this.viewMode) {
                this.propertyForm.disable();
            } else {
                this.propertyForm.enable();
            }
        }
    }

    @Output() closeModal = new EventEmitter<void>();
    @Output() success = new EventEmitter<string>();

    closeModalClicked() {
        this.closeModal.emit();
    }

    onSubmit() {
        if (this.propertyForm.valid) {
            const formValue = this.propertyForm.value;
            const propertyData: RegisterPropertyRequest = {
                title: formValue.title ?? '',
                type: formValue.type ?? '',
                status: formValue.status ?? '',
                price: formValue.price ?? 0,
                owner: formValue.owner ?? '',
                phone: formValue.phone ?? '',
                address: formValue.address ?? '',
                city: formValue.city ?? '',
                state: formValue.state ?? '',
                zipCode: formValue.zipCode ?? '',
                description: formValue.description ?? '',
                khasraNo: formValue.khasraNo ?? '',
                area: formValue.area ?? 0
            };
            if (this.property && this.property.id) {
                // Edit mode
                this.propertyService.updateProperty(this.property.id, propertyData).subscribe({
                    next: () => {
                        this.success.emit('Property updated successfully!');
                    },
                    error: err => this.success.emit('Error: ' + err.message)
                });
            } else {
                // Create mode
                this.propertyService.registerProperty(propertyData).subscribe({
                    next: () => {
                        this.success.emit('Property registered successfully!');
                    },
                    error: err => this.success.emit('Error: ' + err.message)
                });
            }
        }
    }

    showMessage(msg: string) {
        this.messageText = msg;
        this.messageBoxVisible = true;
    }

    closeMessageBox() {
        this.messageBoxVisible = false;
    }
}
