import { Component } from '@angular/core';
import { ProjectsService, Ques } from '../projects.service';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent {
  ques: Ques[];
  dataSource: any;
  // data: any;
  // question: string[];
  // showFilterRow: boolean;
  tab1: any;

  constructor(private projectsService: ProjectsService) {
    // TODO: Edit popup dropdown
    this.ques = this.projectsService.getQues();
    this.dataSource = {
      store: {
        type: 'array',
        key: 'ID',
        data: this.projectsService.getQues()
      }
    };
  }

  // ('.rating-star').click(function() {
  //     this.parents('.rating').find('.rating-star').removeClass('checked');
  //     this.addClass('checked');

  //     var submitStars = this.attr('data-value');
  //     alert(submitStars);
  // });
}
