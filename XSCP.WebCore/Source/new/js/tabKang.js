function getID(ID){return document.getElementById(ID);}
function Ds(i){getID(i).style.display='';}
function Dh(i){getID(i).style.display='none';}
function Tb(d,t,p,c){for(var i=1;i<=t;i++){if(d==i)
{getID(p+'_t_'+i).className=c+'_2';Ds(p+'_c_'+i);}
else
{getID(p+'_t_'+i).className=c+'_1';Dh(p+'_c_'+i);}}}
function resizepic(ThisPic){var RePicWidth=650;var TrueWidth=ThisPic.width;var TrueHeight=ThisPic.height;var Multiple=TrueWidth/RePicWidth;ThisPic.width=RePicWidth;ThisPic.height=TrueHeight/Multiple;}