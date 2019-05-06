clear;
clc;
close all;

A=[1.05,1.75,1.45,1.35,2.15,3.05,4.15,2.55,2.95,3.15];

standard_1=var(A,0)

standard_2=var(A,1)

% plot(A,'-k','linewidth',2,'Marker','o','MarkerFace','y','MarkerEdge','r','MarkerSize',10);
% hold on
% plot([0:0.1:10],standard_1,'r.','MarkerSize',5)
% 
% plot([0:0.1:10],standard_2,'c.','MarkerSize',5)
% 
% legend('data','standard deviation 1','standard deviation 2',2)

for i=1:length(A)
    result = GetVariance(A(i));
end

result
    