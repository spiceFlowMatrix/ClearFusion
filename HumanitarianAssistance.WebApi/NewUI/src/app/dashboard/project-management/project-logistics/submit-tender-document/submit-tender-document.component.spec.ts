import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitTenderDocumentComponent } from './submit-tender-document.component';

describe('SubmitTenderDocumentComponent', () => {
  let component: SubmitTenderDocumentComponent;
  let fixture: ComponentFixture<SubmitTenderDocumentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitTenderDocumentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitTenderDocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
