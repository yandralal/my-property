import { Component, Output, EventEmitter, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AgentService } from '../services/agent.service';
import { Agent } from '../models/agent.model';

@Component({
  selector: 'app-agent-form',
  templateUrl: './agent-form.component.html',
  styleUrls: ['./agent-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class AgentFormComponent implements OnInit {
  @Input() agentId?: number;
  @Input() agent: any;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<any>();

  agentForm: FormGroup;
  isSubmitting = false;

  constructor(private fb: FormBuilder, private agentService: AgentService) {
    this.agentForm = this.fb.group({
      name: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/), Validators.maxLength(10)]],
      agency: ['']
    });
  }

  ngOnInit() {
    if (this.agent) {
      this.agentForm.patchValue({
        name: this.agent.name,
        phone: this.agent.contact,
        agency: this.agent.agency
      });
    } else if (this.agentId) {
      this.agentService.getAgentById(this.agentId).subscribe(agent => {
        this.agentForm.patchValue({
          name: agent.name,
          phone: agent.contact,
          agency: agent.agency
        });
      });
    }
  }

  closeModalClicked() {
    this.closeModal.emit();
  }

  onSubmit() {
    if (this.agentForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      const formValue = this.agentForm.value;
      if (this.agent && this.agent.id) {
        // Edit agent
        this.agentService.updateAgent(this.agent.id, {
          name: formValue.name,
          contact: formValue.phone,
          agency: formValue.agency
        }).subscribe({
          next: (agent) => {
            this.success.emit(agent);
            this.closeModalClicked();
            this.isSubmitting = false;
          },
          error: () => {
            this.isSubmitting = false;
          }
        });
      } else {
        // Create agent
        this.agentService.createAgent({
          name: formValue.name,
          contact: formValue.phone,
          agency: formValue.agency
        }).subscribe({
          next: (agent) => {
            this.success.emit(agent);
            this.closeModalClicked();
            this.isSubmitting = false;
          },
          error: () => {
            this.isSubmitting = false;
          }
        });
      }
    } else {
      this.agentForm.markAllAsTouched();
    }
  }
}