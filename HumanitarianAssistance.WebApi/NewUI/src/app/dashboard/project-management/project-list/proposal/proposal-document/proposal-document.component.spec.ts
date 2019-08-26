import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProposalDocumentComponent } from './proposal-document.component';

describe('ProposalDocumentComponent', () => {
  let component: ProposalDocumentComponent;
  let fixture: ComponentFixture<ProposalDocumentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProposalDocumentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProposalDocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
